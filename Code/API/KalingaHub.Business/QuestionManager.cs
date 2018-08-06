using KalingaHub.DataAccess;
using KalingaHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalingaHub.Business
{
    public class QuestionManager
    {
        readonly QuestionRepository _questionRepository;
        readonly TagRepository _tagRepository;

        public QuestionManager()
        {
            _questionRepository = new QuestionRepository();
            _tagRepository = new TagRepository();
        }

        public QuestionModel GetQuestionWithAnswers(Guid questionId)
        {
            var question = _questionRepository.GetQuestion(questionId);
            var answers = _questionRepository.GetAnswers(questionId);
            var questionModel = new QuestionModel();
            questionModel.Id = questionId;
            questionModel.Title = question.Title;
            questionModel.Tags = _questionRepository.GetQuestionTags(questionId);
            //questionModel.Answers = answers;
            questionModel.Description = question.Description;
            return questionModel;
        }

        public Response PostQuestion(QuestionModel question)
        {
            var id = Guid.NewGuid();
            var response = _questionRepository.InsertQuestion(id, question);
            return response;
        }
        /// <summary>
        /// EditQuestion method modifies the Question,adds new tags and removes unwanted tags
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public Response EditQuestion(QuestionModel question)
        {
            var response = new Response();
            try
            {

                response = _questionRepository.EditQuestion(question);
                if (response.IsSuccess)
                {
                    List<string> originalTags = _questionRepository.GetQuestionTags(question.Id);
                    List<string> newTags = question.Tags;

                    List<string> addTags = newTags.Except(originalTags).ToList();
                    _tagRepository.AddTags(addTags);
                    List<Guid> addedTagIds = _tagRepository.GetTags(addTags).Select(x => x.Id).ToList();
                    var res = _questionRepository.AddQuestionTags(addedTagIds, question.Id);
                    List<string> removeTags = originalTags.Except(newTags).ToList();
                    List<Guid> removeIds = _tagRepository.GetTags(removeTags).Select(x => x.Id).ToList();
                    var res1 = _questionRepository.RemoveQuestionTags(removeIds, question.Id);
                    response.IsSuccess = true;
                    response.Message = "Question Edited Successfully..";
                    return response;
                }
                else
                {
                    
                    ///response.Message = "something went wrong..";
                    return response;
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.ToString();
                return response;
            }


        }
    }
}

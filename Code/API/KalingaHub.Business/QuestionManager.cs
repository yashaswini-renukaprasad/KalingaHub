using KalingaHub.DataAccess;
using KalingaHub.DataAccess.Entities;
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

        readonly TagManager _tagManager;

        public QuestionManager()
        {
            _questionRepository = new QuestionRepository();
            _tagRepository = new TagRepository();

            _tagManager = new TagManager();
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
            Response response = new Response();
            try
            {
                var id = Guid.NewGuid();
                response = _questionRepository.InsertQuestion(id, question);
                System.Console.WriteLine(response.IsSuccess);
                if (response.IsSuccess)
                {
                    var newTags = question.Tags;        //get tagnames from the question
                    var existingTagNames = _tagRepository.GetTags(newTags).Select(x => x.Name).ToList();        //return tagnames of the existing tags
                    var addTags = newTags.Except(existingTagNames).ToList();        //get tagnames that doesnot exist in tag
                    var tagIds = _tagManager.AddTags(addTags);          //add tags to tag and get tagids of new tags
                    response = _questionRepository.AddQuestionTags(tagIds, id);         //add tagids and questionid to the questiontag
                    response.Message = "Question and Tags Successfully Posted";         //setting message in response
                }
                return response;
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;

                return response;
            }
        }

        public void EditQuestion(QuestionModel question)
        {
            _questionRepository.EditQuestion(question);
            List<string> originalTags = _questionRepository.GetQuestionTags(question.Id);
            List<string> newTags = question.Tags;

            List<string> addTags = newTags.Except(originalTags).ToList();
            List<Guid> addedTagIds = _tagRepository.GetTags(addTags).Select(x => x.Id).ToList();
            _questionRepository.AddQuestionTags(addedTagIds, question.Id);

            List<string> removeTags = originalTags.Except(newTags).ToList();
            List<Guid> removeIds = _tagRepository.GetTags(removeTags).Select(x => x.Id).ToList();
            _questionRepository.RemoveQuestionTags(removeIds, question.Id);
        }
    }
}

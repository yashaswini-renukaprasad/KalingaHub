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

        public QuestionManager()
        {
            _questionRepository = new QuestionRepository();
            _tagRepository = new TagRepository();
        }

        public QuestionModel GetQuestionWithAnswers(Guid questionId)
        {
           
            var question = _questionRepository.GetQuestion(questionId);
            if(question==null)
            {
                return null;
            }
            var questionModel = new QuestionModel();
            var answers = _questionRepository.GetAnswers(questionId);
            if (answers == null)
                questionModel.Answers = null; 
            else
            {
               questionModel.Answers = answers;
            }
            
            questionModel.Id = questionId;
            questionModel.Title = question.Title;
            questionModel.Tags = _questionRepository.GetQuestionTags(questionId);
            questionModel.Description = question.Description;
            questionModel.Tags = _questionRepository.GetQuestionTags(questionId);
            questionModel.Category = _questionRepository.GetQuestionCategory(questionId);
            return questionModel;
        }

        public Response PostQuestion(QuestionModel question)
        {
            var id = Guid.NewGuid();
            var response = _questionRepository.InsertQuestion(id, question);
            return response;
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

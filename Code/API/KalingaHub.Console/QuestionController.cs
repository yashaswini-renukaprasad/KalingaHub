using KalingaHub.Business;
using KalingaHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalingaHub.Console
{
    public class QuestionController
    {
        readonly QuestionManager _questionManager;

        public QuestionController()
        {
            _questionManager = new QuestionManager();
        }

        public QuestionModel Get(Guid questionId)
        {
            if (questionId == Guid.Empty)
            {
                //return new Response { IsSuccess = false, Message = "Invalid input" };
                return null;
            }
            return _questionManager.GetQuestionWithAnswers(questionId);
        }

        public Response Post(QuestionModel question)
        {
            var response = _questionManager.PostQuestion(question);
            return response;
        }
        /// <summary>
        /// Edits question by taking control from manager
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public Response Put(QuestionModel question)
        {
            if (question.Id == Guid.Empty)
            {
               return new Response { IsSuccess = false, Message = "Invalid input" };
                
            }
            var response = _questionManager.EditQuestion(question);
            return response;
        }
    }
}

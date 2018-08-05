using KalingaHub.Business;
using KalingaHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace KalingaHub.WebAPI.Controllers
{
    public class QuestionController:ApiController
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

        public Response Put(QuestionModel question)
        {
            if (question.Id == Guid.Empty)
            {
                //return new Response { IsSuccess = false, Message = "Invalid input" };
                return null;
            }
            var response = _questionManager.PostQuestion(question);
            return response;
        }
    }
}

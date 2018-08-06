using KalingaHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalingaHub.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            QuestionModel _questionModel = new QuestionModel
            {
                Id = new Guid("cb98c976-6a9a-49eb-a44a-3423d0e3c056")
            };
            System.Console.WriteLine("enter title:");
            _questionModel.Title = System.Console.ReadLine();
            System.Console.WriteLine("enter description:");
            _questionModel.Description = System.Console.ReadLine();
            _questionModel.CategoryId = Guid.NewGuid();
            _questionModel.Tags = new List<string>() { "hello", "abc" };
            _questionModel.CreatedBy = Guid.NewGuid();
            _questionModel.CreatedDate = DateTime.Now;
            _questionModel.IsActive = true;
            QuestionController _questionController = new QuestionController();
            var response = _questionController.Put(_questionModel);
            System.Console.WriteLine(response.Message);
            System.Console.ReadLine();
        }
    }
}

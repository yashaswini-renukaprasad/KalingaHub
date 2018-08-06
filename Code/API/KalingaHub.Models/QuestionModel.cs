using System;
using System.Collections.Generic;

namespace KalingaHub.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class QuestionModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<AnswerModel> Answers { get; set; }
        /// <summary>
        /// Category of the Question
        /// </summary>
        public Guid CategoryId { get; set; }
        /// <summary>
        /// Category of the Question
        /// </summary>
        public CategoryModel Category { get; set; }
        /// <summary>
        /// Tag list of the Question
        /// </summary>
        public List<string> Tags { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }


        public int UpvoteCount { get; set; }
        public bool IsActive { get; set; }

    }

}

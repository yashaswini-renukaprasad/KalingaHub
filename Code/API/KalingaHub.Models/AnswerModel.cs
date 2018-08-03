﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalingaHub.Models
{
    public class AnswerModel
    {
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }

       
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Guid CreatedBy { get; set; }
    }
}

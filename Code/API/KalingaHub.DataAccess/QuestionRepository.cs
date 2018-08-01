using KalingaHub.DataAccess.Entities;
using KalingaHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KalingaHub.Models.SearchParameters;

namespace KalingaHub.DataAccess
{
    public class QuestionRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public Question GetQuestion(Guid questionId)
        {
            using (var db = new KalingaHubDBModel())
            {
                var question = (from q in db.Questions
                                      where q.Id == questionId && q.IsActive == true
                                      select q).SingleOrDefault();
                return question;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public List<Answer> GetAnswers(Guid questionId)
        {
            using (var db = new KalingaHubDBModel())
            {
                var answers = (from q in db.Answers
                                     where q.Id == questionId //&& q.IsActive == true
                               select q)?.ToList<Answer>();
                return answers;
            }
        }

        public Response InsertQuestion(Guid id, QuestionModel question)
        {
            var userId = Guid.NewGuid();
            var response = new Response();
            using (var db = new KalingaHubDBModel())
            {
                try
                {
                    db.Questions.Add(new Question
                    {
                        Id = id,
                        CreatedBy = userId,
                        CreatedDate = DateTime.Now,
                        Title = question.Title,
                        Description = question.Description,
                        CategoryId = question.CategoryId
                    });
                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    response.IsSuccess = false;
                    response.Message = e.ToString();
                    return response;
                }

                response.IsSuccess = true;
                response.Message = "Question Successfully Posted";
                return response;
            }
        }

        public void EditQuestion(QuestionModel questionModel)
        {
            using (var db = new KalingaHubDBModel())
            {
                var question = db.Questions.Single(q => q.Id == questionModel.Id && q.IsActive);
                question.ModifiedDate = DateTime.Now;
                question.Title = questionModel.Title;
                question.Description = questionModel.Description;
                db.SaveChanges();
            }
        }

        public List<string> GetQuestionTags(Guid questionId)
        {
            List<string> questionTags = new List<string>();
            using (var db = new KalingaHubDBModel())
            {
                var qry = from t in db.QuestionTags
                          where t.QuestionId == questionId
                          select t.TagId;
                foreach (var obj in qry)
                {
                    var qry1 = (from t in db.Tags
                                where t.Id == obj
                                select t.Name).FirstOrDefault();
                    questionTags.Add(qry1);
                }
            }
            return questionTags;
        }

        public void AddQuestionTags(List<Guid> tagIds, Guid questionId)
        {
            using (var db = new KalingaHubDBModel())
            {
                foreach (var tagId in tagIds)
                {
                    QuestionTag questionTag = new QuestionTag { QuestionId = questionId, TagId = tagId };
                    db.QuestionTags.Add(questionTag);
                }
                db.SaveChanges();
            }
        }

        public void RemoveQuestionTags(List<Guid> tagIds, Guid questionId)
        {
            using (var db = new KalingaHubDBModel())
            {
                var questionTags = (from qt in db.QuestionTags
                              where tagIds.Contains(qt.TagId) && qt.QuestionId == questionId
                              select qt)?.ToList();
                db.QuestionTags.RemoveRange(questionTags);
                db.SaveChanges();
            }
        }

        public IEnumerable<QuestionModel> Search(SearchParameters searchParameters)
        { 
            using (var db = new KalingaHubDBModel())
            {
                IQueryable<Question> query = null;
                query =
                    db.Questions
                    .Where(x =>
                        (searchParameters.Filter.Categories != null
                        && searchParameters.Filter.Categories.Equals(x.CategoryId)))
                    .Where(x =>
                        (searchParameters.Filter.Keyword != null
                        && x.Title.Contains(searchParameters.Filter.Keyword)))
                    .Where(x =>
                        (searchParameters.Filter.Keyword != null
                        && x.Description.Contains(searchParameters.Filter.Keyword)))
                ;

                if (searchParameters.Filter.TagList != null && searchParameters.Filter.TagList.Count > 0)
                {
                    var tags_matched = db.Tags
                        .Where(x => x.Name != null &&
                        x.Name.Length > 0 &&
                        searchParameters.Filter.TagList.Contains(x.Name))
                    ;

                    query = query.Join(db.QuestionTags,
                            question => question.Id,
                            tagmap => tagmap.QuestionId,
                            (question, tagmap) => new
                            {
                                question,
                                tagmap
                            }
                        ).Join(tags_matched,
                        question_tagmap => question_tagmap.tagmap.TagId,
                        tag => tag.Id,
                        (question_tagmap, tag) =>
                            question_tagmap.question
                        )
                    ;
                }

                // Join with Upvote table and return resulted data.
                var questionsWithUpvote = query
                    .Select(x =>
                       new QuestionModel
                       {
                           Id = x.Id,
                           CategoryId = x.CategoryId,
                           CreatedBy = x.CreatedBy,
                           CreatedDate = x.CreatedDate,
                           Description = x.Description,
                           IsActive = x.IsActive,
                           ModifiedDate = x.ModifiedDate,
                           Title = x.Title,
                           UpvoteCount =
                               (from upvotes in db.Upvotes
                                where upvotes.ResourceId == x.Id
                                select upvotes)
                               .Count()
                       });
                if (searchParameters.OrderBy == E_SEARCHORDER.ORDER_LATEST)
                {
                    questionsWithUpvote =
                        questionsWithUpvote
                        .OrderByDescending(x => x.CreatedDate);
                }
                else
                {
                    questionsWithUpvote = 
                        questionsWithUpvote
                        .OrderByDescending(c => c.UpvoteCount)
                        .ThenByDescending(x => x.CreatedDate)
                    ; 
                } 
                return questionsWithUpvote.ToList();
            }
        }
    }
}

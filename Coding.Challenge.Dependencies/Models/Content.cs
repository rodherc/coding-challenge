using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding.Challenge.Dependencies.Models
{
    public class Content
    {
        public Guid Id { get; }
        public string Title { get; }
        public string SubTitle { get; }
        public string Description { get; }
        public string ImageUrl { get; }
        public int Duration { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public IEnumerable<string> GenreList { get; }


        public Content(Guid id, string title, string subTitle, string description, string imageUrl, int duration, DateTime startTime, DateTime endTime, IEnumerable<string> genreList)
        {
            Id = id;
            Title = title;
            SubTitle = subTitle;
            Description = description;
            ImageUrl = imageUrl;
            Duration = duration;
            StartTime = startTime;
            EndTime = endTime;
            GenreList = genreList;
        }
    }
}

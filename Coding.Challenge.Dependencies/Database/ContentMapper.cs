using Coding.Challenge.Dependencies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding.Challenge.Dependencies.Database
{
    public class ContentMapper : IMapper<Content, ContentDto>
    {
        public Content Map(Guid id, ContentDto item)
        {
            return new Content(
            id,
            item.Title ?? throw new ArgumentNullException(nameof(item.Title)),
            item.SubTitle ?? throw new ArgumentNullException(nameof(item.SubTitle)),
            item.Description ?? throw new ArgumentNullException(nameof(item.Description)),
            item.ImageUrl ?? throw new ArgumentNullException(nameof(item.ImageUrl)),
            item.Duration ?? throw new ArgumentNullException(nameof(item.Duration)),
            item.StartTime ?? throw new ArgumentNullException(nameof(item.StartTime)),
            item.EndTime ?? throw new ArgumentNullException(nameof(item.EndTime)),
            item.GenreList);
        }

        public Content Patch(Content oldItem, ContentDto newItem)
        {
            return new Content(
                oldItem.Id,
                newItem.Title ?? oldItem.Title,
                newItem.SubTitle ?? oldItem.SubTitle,
                newItem.Description ?? oldItem.Description,
                newItem.ImageUrl ?? oldItem.ImageUrl,
                newItem.Duration ?? oldItem.Duration,
                newItem.StartTime ?? oldItem.StartTime,
                newItem.EndTime ?? oldItem.EndTime,
                !newItem.GenreList.Any() ? oldItem.GenreList : newItem.GenreList);
        }
    }
}

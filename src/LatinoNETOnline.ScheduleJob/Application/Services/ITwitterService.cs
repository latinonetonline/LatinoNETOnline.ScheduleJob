using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LatinoNETOnline.ScheduleJob.Application.Services
{
    public interface ITwitterService
    {
        Task<Uri> CreateTweet(string tweetText, byte[] image);
    }
}

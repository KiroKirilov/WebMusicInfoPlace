﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Services;
using WMIP.Services.Dtos.Comments;
using WMIP.Tests.Common;
using Xunit;

namespace WMIP.Tests
{
    public class CommentsServiceTests : BaseTestClass
    {
        [Fact]
        public void Create_AddsCommentToDb()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var commentsService = new CommentsService(context);
            var creationInfo = new CreateCommentDto
            {
                Body = "bod",
                Title = "title",
                PostId = 1,
                UserId = "1"
            };

            // Act
            commentsService.Create(creationInfo, out Comment comment);

            //Assert
            Assert.Single(context.Comments);
            Assert.NotNull(comment);
        }

        [Fact]
        public void GetCommentsByUser_ReturnsCorrectComments()
        {
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var comment1 = new Comment { UserId = "1" };
            var comment2 = new Comment { UserId = "2" };
            context.Comments.AddRange(comment1, comment2);
            context.SaveChanges();
            var commentsService = new CommentsService(context);

            // Act
            var results = commentsService.GetCommentsByUser("1");

            //Assert
            Assert.Single(results);
            Assert.Equal("1", results.First().UserId);
        }
    }
}

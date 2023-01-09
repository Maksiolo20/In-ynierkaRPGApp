﻿using Microsoft.EntityFrameworkCore;
using RPGApp.Data;
using RPGApp.Interfaces;
using RPGApp.Models;
using System.Security.Claims;

namespace RPGApp.Services
{
	public class InvitePlayerService :IPlayer
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private string UserId;
		public InvitePlayerService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context=context;
			_httpContextAccessor=httpContextAccessor;
			UserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
		}
		public List<string> GetPlayers()
		{
			List<string> result = new List<string>();
			var currentSessionId = _context.Users.First(x => x.Id == UserId).CurrentSessionId;
			var sessionToGet = _context.Sessions.SingleOrDefault(x => x.Id == currentSessionId);
			foreach (var item in sessionToGet.Players)
			{
				result.Add(item);
			}
			return result;
		}
		public void AddPlayer(string player)
		{
			int sessionId = _context.Users.First(x => x.Id == UserId).CurrentSessionId;
			var result = _context.Sessions.SingleOrDefault(x => x.Id == sessionId);
			bool doesPlayerExists = _context.Users.Any(x => x.Email == player);
			if (doesPlayerExists)
			{
				result.Players.Add(player);
				_context.SaveChanges();
			}
		}

	}
}

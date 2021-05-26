using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Database;
using Backend.GameLogics;
using Backend.Model;
using GameEngine.GameLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Services
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddMyServices(this IServiceCollection services)
        {
            services.AddScoped<ICreateNewGame, CreateNewGame>()
                .AddScoped<IGameSession, GameSession>()
                .AddScoped<IPawn, Pawn>()
                .AddScoped<IPawnFinishLinePosition, PawnFinishLinePosition>()
                .AddScoped<IPawnStartPosition, PawnStartPosition>()
                .AddScoped<IRotatePlayer, RotatePlayer>()
                .AddScoped<IMovingPawn, MovingPawn>()
                .AddScoped<IDbQueries, DbQueries>()
                .AddScoped<IFindPawn, FindPawn>()
                .AddScoped<IKnockPawn, KnockPawn>()
                .AddScoped<INewPawnPosition, NewPawnPosition>()
                .AddScoped<IPawn, Pawn>()
                .AddScoped<IDisplayMessage, DisplayMessage>()
                .AddDbContext<LudoContext>(option =>
                    option.UseSqlServer(
                        @"Server=localhost,41433;Database=LudoV2;User ID=sa; Password=verystrong!pass321"));
            return services;
        }
    }
}
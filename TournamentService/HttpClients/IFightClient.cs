﻿using System.Threading.Tasks;
using Refit;
using TournamentService.Models;

namespace TournamentService.HttpClients
{
  internal interface IFightClient
  {
    [Post("/api/fight")]
    Task<BattleInfo> StartNew([Body] string[] dockerImages);
  }
}
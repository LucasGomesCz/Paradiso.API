global using Microsoft.EntityFrameworkCore;

global using System.Globalization;

global using Azure.Storage.Blobs;

global using Microsoft.AspNetCore.Http;

global using AutoMapper;

global using Paradiso.API.Domain.Dtos;
global using Paradiso.API.Domain.Entities;
global using Paradiso.API.Domain.Interfaces;
global using Paradiso.API.Domain.Enums;

global using Paradiso.API.Domain.Models.Movies;
global using Paradiso.API.Domain.Models.Photos;
global using Paradiso.API.Domain.Models.Scripts;
global using Paradiso.API.Domain.Models.SoundTracks;
global using Paradiso.API.Domain.Models.Users;
global using Paradiso.API.Domain.Models.Shared;

global using static Paradiso.API.Domain.Models.Auxiliars.AuxiliarGetParams;

global using Paradiso.API.Infra.Context;

global using Paradiso.API.Service.Utils;
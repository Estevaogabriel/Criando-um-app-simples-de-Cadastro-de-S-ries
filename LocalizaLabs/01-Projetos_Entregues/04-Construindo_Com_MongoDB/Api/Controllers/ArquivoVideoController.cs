﻿using Api.Data.Collections;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

//https://localhost:5001/arquivoVideo
namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArquivoVideoController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<ArquivoVideo> _arquivoVideosCollection;

        public ArquivoVideoController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _arquivoVideosCollection = _mongoDB.DB.GetCollection<ArquivoVideo>(typeof(ArquivoVideo).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarArquivoVideo([FromBody] ArquivoVideoDTO dto)
        {
            var arquivoVideo = new ArquivoVideo(dto.CodigoBarras, dto.DataLancamento, dto.Genero, dto.Sinpse);

            _arquivoVideosCollection.InsertOne(arquivoVideo);

            return StatusCode(201, "Arquivo de Vídeo adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterArquivoVideo( )
        {
            var arquivoVideo = _arquivoVideosCollection.Find(Builders<ArquivoVideo>.Filter.Empty).ToList();

            return Ok(arquivoVideo);
        }
    }
}

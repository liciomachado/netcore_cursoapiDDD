﻿using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.Uf;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test.Uf
{
    public class QuandoForExecutadoGet: UfTestes
    {
        private IUfService _service;
        private Mock<IUfService> _serviceMock;

        [Fact(DisplayName = "é possivel executar o método Get.")]
        public async Task E_possivel_executar_metodo_get()
        {
            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(m => m.Get(IdUf)).ReturnsAsync(ufDto);
            _service = _serviceMock.Object;

            var result = await _service.Get(IdUf);
            Assert.NotNull(result);
            Assert.True(result.Id == IdUf);
            Assert.Equal(Nome, result.Nome);

            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(Task.FromResult((UfDto)null));
            _service = _serviceMock.Object;

            var _record = await _service.Get(IdUf);
            Assert.Null(_record);
        }
    }
}

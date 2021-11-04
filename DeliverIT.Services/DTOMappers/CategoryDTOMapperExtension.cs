using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIT.Services.DTOMappers
{
    public static class CategoryDTOMapperExtension
    {
        public static CategoryDTO GetDTO(this Category category)
        {

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public static Category GetEntity(this CategoryDTO category)
        {
            return new Category
            {
                Id = category.Id,
                Name = category.Name,
            };
        }
    }
}

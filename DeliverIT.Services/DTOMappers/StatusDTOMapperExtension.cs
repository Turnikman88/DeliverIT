using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.DTOs;

namespace DeliverIT.Services.DTOMappers
{
    public static class StatusDTOMapperExtension
    {
        public static StatusDTO GetDTO(this Status status)
        {

            return new StatusDTO
            {
                Id = status.Id,
                Name = status.Name
            };
        }

        public static Status GetEntity(this StatusDTO status)
        {
            return new Status
            {
                Id = status.Id,
                Name = status.Name,
            };
        }
    }
}

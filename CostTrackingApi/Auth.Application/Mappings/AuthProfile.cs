using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Application.Features.Auth.Queries;
using Auth.Application.Parameters.Auth;
using Auth.Domain.Entities;
using AutoMapper;


namespace Auth.Application.Mappings
{
    public class AuthProfile : Profile
    {

        public AuthProfile()
        {

            CreateMap<GetAllUsersQuery, KeycloakUser>();
      

            CreateMap<KeycloakUser, GetAuthParameter>();
            CreateMap < GetAllUsersQuery, GetAuthParameter > ();
            CreateMap<GetAllRolesQuery, GetAuthParameter>();

        }

    }
}

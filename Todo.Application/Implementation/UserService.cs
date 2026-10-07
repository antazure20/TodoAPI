using AutoMapper;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Todo.Application.Contracts;
using Todo.Application.DTOs.Request;
using Todo.Domain.DomainEntities;
using Todo.Domain.RepositoryInterfaces;

namespace Todo.Application.Implementation
{
    public class UserService (IUserRepository userRepository, IMapper mapper) : IUserService
    {
        //private readonly IUserRepository userRepository;

        //public UserService(IUserRepository userRepository)
        //{
        //    this.userRepository = userRepository;
        //}

        public async Task<bool> CreateUserAsync(CreateUserDto userDto)
        {
            // dto into domain
            var userDomain = mapper.Map<UserDomain>(userDto);

            // password into hashed password
            userDomain.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDomain.PasswordHash);

            await userRepository.AddAsync(userDomain);

            var response = await userRepository.CommitAsync();

            return response > 0;  // Devuelve true si se guardó correctamente, false si no
            
        }
    }
}

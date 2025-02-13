﻿using Duende.IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebShop.IdentityServer.Configuration;
using WebShop.IdentityServer.Data;

namespace WebShop.IdentityServer.SeedDataBase
{
    public class DatabaseIdentityServerInitializer : IDataBaseSeedInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseIdentityServerInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void InitializerSeedRoles()
        {
            //Se o Perfil Admin não existir então cria o perfil
            if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Admin).Result)
            {
                //cria o perfil Admin
                IdentityRole roleAdmin = new IdentityRole();
                roleAdmin.Name = IdentityConfiguration.Admin;
                roleAdmin.NormalizedName = IdentityConfiguration.Admin.ToUpper();
                _roleManager.CreateAsync(roleAdmin).Wait();
            }
            //Se o Perfil Client não existir então cria o perfil
            if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Client).Result)
            {
                //cria o perfil Client
                IdentityRole roleClient = new IdentityRole();
                roleClient.Name = IdentityConfiguration.Client;
                roleClient.NormalizedName = IdentityConfiguration.Client.ToUpper();
                _roleManager.CreateAsync(roleClient).Wait();
            }
        }

        public void InitializerSeedUsers()
        {
            //Se o usuário Admin não existir cria o usuário, define a senha e atribui ao perfil
            if (_userManager.FindByEmailAsync("admin1@com.br").Result == null)
            {
                ApplicationUser admin = new ApplicationUser()
                {
                    UserName = "admin1",
                    NormalizedUserName = "ADMIN1",
                    Email = "admin1@com.br",
                    NormalizedEmail = "ADMIN1@COM.BR",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (11) 12345-6789",
                    FirstName = "Usuario",
                    LastName = "Admin1",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                //cria o usuário Admin e atribui a senha
                IdentityResult resultAdmin = _userManager.CreateAsync(admin, "Numsey#2025").Result;
                if (resultAdmin.Succeeded)
                {
                    //atribui o perfil Admin ao usuário Admin
                    _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).Wait();

                    //inclui as claims do usuário admin
                    var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
                }).Result;
                }
            }
            if (_userManager.FindByEmailAsync("client1@com.br").Result == null)
            {
                //define os dados do usuário client
                ApplicationUser client = new ApplicationUser()
                {
                    UserName = "client1",
                    NormalizedUserName = "CLIENT1",
                    Email = "client1@com.br",
                    NormalizedEmail = "CLIENT1@COM.BR",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (11) 12345-6789",
                    FirstName = "Usuario",
                    LastName = "Client1",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                //cria o usuário Client e atribui a senha
                IdentityResult resultClient = _userManager.CreateAsync(client, "Numsey#2022").Result;
                //inclui o usuário Client ao perfil Client
                if (resultClient.Succeeded)
                {
                    _userManager.AddToRoleAsync(client, IdentityConfiguration.Client).Wait();

                    //adiciona as claims do usuário Client
                    var clientClaims = _userManager.AddClaimsAsync(client, new Claim[]
                    {
                    new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, client.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, client.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
                    }).Result;
                }
            }
        }
    }
}

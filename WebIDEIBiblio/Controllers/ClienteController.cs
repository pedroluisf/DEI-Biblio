using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BiblioModel.Model;

namespace WebIDEIBiblio.Controllers
{
    public class ClienteController
    {
        public static void RegistarCliente(string username, string nome, string pwd, string confirmaPwd, string email, string morada, string funcao)
        {
            // server side check
            if (username == "") throw new ApplicationException("Username é um campo obrigatório.");
            if (pwd == "") throw new ApplicationException("Password é um campo obrigatório.");
            if (pwd != confirmaPwd) throw new ApplicationException("A password e a confirmação não coincidem.");
            if (nome == "") throw new ApplicationException("Nome é um campo obrigatório.");
            if (email == "") throw new ApplicationException("Email é um campo obrigatório.");
            if (morada == "") throw new ApplicationException("Email é um campo obrigatório.");
            if (funcao == "") throw new ApplicationException("Função é um campo obrigatório.");
            
            // criar o membershipUser
            MembershipUser u = Membership.CreateUser(username, pwd, email);
            if (u == null) throw new ApplicationException("Não foi possível adicionar um novo utilizador.");

            // atribuir o role Utilizador
            try
            {
                RemoveUserFromAllRoles(u.UserName);
                Roles.AddUserToRole(u.UserName, funcao);
            }
            catch (Exception)
            {
                Membership.DeleteUser(u.UserName);
                throw new ApplicationException("Não foi possível adicionar o novo utilizador ao papel utilizador.");
            }
            
            // criar o cliente
            Cliente c = new Cliente();
            c.Nome = nome;
            c.Morada = morada;
            c.Email = email;
            c.MembershipId = u.ProviderUserKey.ToString();

            if (c.Validate())
            {
                c.Save();
            }
        }

        public static void RemoverCliente(int id)
        {
            Cliente c = Cliente.findById(id);
            MembershipUser u;
            
            if (c == null) throw new ApplicationException("Não foi possível carregar o cliente indicado.");

            u = Membership.GetUser(System.Guid.Parse(c.MembershipId));
            
            if (u == null) throw new ApplicationException("Não foi possível carregar o utilizador indicado.");
            
            if (!Membership.DeleteUser(u.UserName, true)) throw new ApplicationException("Não foi possível eliminar o utilizador indicado.");
            
            if (c.Remove() != 1) throw new ApplicationException("Não foi possível eliminar o cliente indicado.");
        }

        public static void AssociarMembershipCliente(string username, string email, string membershipId)
        {
            Cliente c = new Cliente();
            c.Nome = username;
            c.Email = email;
            c.Morada = "";
            c.MembershipId = membershipId;

            if (c.Validate())
            {
                c.Save();
            }
        }

        public static void ActualizarCliente(int id, string nome, string email, string morada, string funcao)
        {            
            Cliente c = Cliente.findById(id);
            if (c == null) throw new ApplicationException("Não foi possível carregar o cliente indicado.");

            MembershipUser u = Membership.GetUser(Guid.Parse(c.MembershipId));
            if (u == null) throw new ApplicationException("Não foi possível carregar o utilizador indicado.");

            // por falta de tempo estamos a assumir que um utilizador apenas poderá ocupar uma função
            RemoveUserFromAllRoles(u.UserName);
            Roles.AddUserToRole(u.UserName, funcao);
            
            c.Nome = nome;
            c.Email = email;
            c.Morada = morada;

            if (c.Validate())
            {
                c.Save();
            }
        }

        private static void RemoveUserFromAllRoles(string username)
        {
            string[] currentRoles;
            currentRoles = Roles.GetRolesForUser(username);
            foreach (string role in currentRoles)
            {
                Roles.RemoveUserFromRole(username, role);
            }
        }
    }
}
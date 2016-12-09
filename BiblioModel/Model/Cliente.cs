using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BiblioModel.DataAccess;

namespace BiblioModel.Model
{
    public class Cliente : ClienteActiveRecord
    {
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Morada
        {
            get { return morada; }
            set { morada = value; }
        }

        public string MembershipId
        {
            get { return membershipId; }
            set { membershipId = value; }
        }

        public Cliente() : base()
        {
        }

        public Cliente(IDataReader reader) : base(reader)
        {

        }

        public int Save()
        {
            if (base.Exists(Id))
            {
                return base.Update();
            }

            return base.Create();
        }

        public Cliente Load(int id)
        {
            //deveria ficar na TableUtilizadores??????
            if (base.Exists(id))
            {
                return base.Read(id);
            }

            return null;
        }

        public int Remove()
        {
            if (base.Exists(this.Id))
            {
                return base.Delete();
            }

            return 0;
        }

        public bool Validate()
        {
            return nome != "" && email != "" && membershipId != "";
        }
    }
}


using Microsoft.EntityFrameworkCore;
using Schedule.domain.Entities;
using Schedule.domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Data.Repositories
{
    //https://localhost:5001/contacts
    public class ContactRepository : IContactRepository
    {

        protected readonly DataContext context;
        public ContactRepository(DataContext context)
        {
            this.context = context;

        }

        public async Task<Contact> GetById(int id)
        {
            try
            {
                var contacts = await context
                    .Contacts.Include(x => x.User)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
                return contacts;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Contact>> GetByUserId(int id)
        {
            var contacts = await context.Contacts.Include(x => x.User).AsNoTracking().Where(x => x.UserId == id).ToListAsync();
            return contacts;
        }

        public async Task<Contact> Post(Contact model)
        {
            try
            {
                context.Contacts.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<Contact>> Put(
            int id,
            Contact model)
        {
            var Contact = await context
                .Contacts
                .FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Entry<Contact>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<Contact>> Delete(
            int id)
        {
            var Contact = await context
                .Contacts
                .FirstOrDefaultAsync(x => x.Id == id);
            try
            {
                context.Contacts.Remove(Contact);
                await context.SaveChangesAsync();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

    }
}


﻿using POC.API.Models;
using POC.DataModel;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace POC.API.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/users
        public IEnumerable<User> Get()
        {
            return UsersDB.GetUsers();
        }

        // GET api/users/?email=tutong@gmail.com
        public IHttpActionResult Get(int Id)
        {
            if (UsersDB.CountUserById(Id) > 0)
                return Json(UsersDB.GetUserById(Id));
            else
                return NotFound();
        }

        // GET api/users/?email=tutong@gmail.com
        public IHttpActionResult Get(string email)
        {
            User user = UsersDB.GetUserByEmail(email);
            if (user == null)
                return NotFound();
            else
                return Json(user);
        }

        // POST api/users/
        public IHttpActionResult Post([FromBody]UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    Name = userModel.Name,
                    Email = userModel.Email,
                    Password = userModel.Password,
                };

                if (UsersDB.Create(user))
                    return Json(user);
            }

            return BadRequest("Data is invalid");
        }

        // PUT api/users/2
        public IHttpActionResult Put (int Id, [FromBody]UserModel userModel)
        {
            if (UsersDB.CountUserById(Id) > 0)
            {
                User user = UsersDB.GetUserById(Id);
                if (userModel.Email != null)
                    user.Email = userModel.Email;
                if (userModel.Name != null)
                    user.Name = userModel.Name;

                if (UsersDB.Update(user))
                    return Json(user);
                else
                    return BadRequest("Data is invalid");
            }
            else
                return NotFound();
        }

        // DELETE api/users/2
        public IHttpActionResult Delete(int Id)
        {
            if (UsersDB.Delete(Id)) {
                return Json(new User() { Id = Id });
            }
            else
                return NotFound();
        }
    }
}

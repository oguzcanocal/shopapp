﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace shopapp.Domain.Entities
{
    public class CartItem
    {

        public int Id { get; set; }

        public int Quantity { get; set; }



        public int ProductId { get; set; }
        public Product Product { get; set; }


        public int CartId { get; set; }
        public Cart Cart { get; set; }


    }
}

﻿
namespace namasdev.Apps.Entidades
{
    public partial class AspNetRole
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}

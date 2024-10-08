 //Interneuron synapse

//Copyright(C) 2024 Interneuron Limited

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

//See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.If not, see<http://www.gnu.org/licenses/>.
﻿namespace SynapsePlatformServices.Models
{
    public class EmailModel
    {
        public string emailFrom { get; set; }
        public string fromName { get; set; }
        public string Username { get; set; }

        public string password { get; set; }

        public string[] emailTo { get; set; }
      
        public string subject { get; set; }

        public string body { get; set; }

    }
   

}

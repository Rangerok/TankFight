﻿using Newtonsoft.Json;

namespace ImageService.Models
{
  public class CreateImageArgs
  {
    public string Language { get; set; }
    public string Code { get; set; }
  }
}
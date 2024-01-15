﻿namespace SportData.Services.Interfaces;

using SportData.Data.Models.Entities.Crawlers;
using SportData.Data.Models.Zip;

public interface IZipService
{
    byte[] ZipGroup(Group group);

    List<ZipModel> UnzipGroup(byte[] data);
}
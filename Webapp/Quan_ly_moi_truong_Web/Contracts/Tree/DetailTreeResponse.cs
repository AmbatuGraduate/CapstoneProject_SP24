﻿namespace Contract.Tree
{
    public record DetailTreeResponse
    (
        string TreeCode,
        string StreetName,
        string Cultivar,
        float BodyDiameter,
        float LeafLength,
        DateTime PlantTime,
        DateTime CutTime,
        string Note
    );
}
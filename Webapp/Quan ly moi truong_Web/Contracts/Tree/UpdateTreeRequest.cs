<<<<<<< HEAD
﻿
namespace Contract.Tree
=======
﻿namespace Contract.Tree
>>>>>>> vu/feature/get-information-of-cultivar
{
    public record UpdateTreeRequest
    (
        Guid StreetId,
        float BodyDiameter,
        float LeafLength,
        DateTime PlantTime,
        int IntervalCutTime,
        Guid CultivarId,
        string Note,
        string UpdateBy,
        bool isExist
    );
}
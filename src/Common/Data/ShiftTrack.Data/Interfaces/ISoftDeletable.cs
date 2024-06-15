﻿namespace ShiftTrack.Data.Interfaces
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedAt { get; set; }
    }
}
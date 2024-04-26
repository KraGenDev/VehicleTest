using System;
using UI;

namespace DTO
{
    [Serializable]
    public record WindowsByType
    {
        public Window Window;
        public WindowType Type;
    }
}
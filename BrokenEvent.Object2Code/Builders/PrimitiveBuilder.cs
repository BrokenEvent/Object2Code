﻿using BrokenEvent.Object2Code.Interfaces;

namespace BrokenEvent.Object2Code.Builders
{
  class PrimitiveBuilder: IBuilder
  {
    public void Build(object target, IBuildContext context)
    {
      context.Append(target.ToString());
    }
  }
}
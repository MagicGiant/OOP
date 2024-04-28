using System;
using Isu.Models;

namespace Isu.Exceptions;
 public class GroupNameException : Exception
 {
     public GroupNameException(string groupName)
         : base($"Group has invalid name: {groupName}")
     { }
 }

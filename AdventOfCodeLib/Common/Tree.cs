using System.Collections.Generic;
using System.Linq;

namespace AdventOfCodeLib.Common {
	public class Tree<T> {
		public Tree<T>? Parent { get; private set; }
		public Tree<T> Head {
			get {
				return (Parent != null) ? Parent.Head : this;
			}
		}
		public bool IsHead {
			get {
				return Head == this;
			}
		}
		public int Depth {
			get {
				return IsHead ? 0 : Parent!.Depth + 1;
			}
		}
		public List<Tree<T>> Children { get; }
		public T Value { get; set; }

		public Tree(T value) {
			Parent = null;
			Children = new List<Tree<T>>();
			Value = value;
		}

		public void AddChild(Tree<T> child) {
			if (!Children.Contains(child)) {
				child.Parent = this;
				Children.Add(child);
			}
		}

		public void AddChild(T child) {
			if (!Children.Exists(c => c.Value!.Equals(child))) {
				var childTree = new Tree<T>(child);
				AddChild(childTree);
			}
		}

		public void RemoveChild(Tree<T> child) {
			if (Children.Remove(child)) {
				child.Parent = null;
			}
		}

		public void RemoveChild(T child) {
			var childTree = Children.Find(c => c.Value!.Equals(child));
			if (childTree != null) {
				RemoveChild(childTree);
			}
		}

		public bool Contains(T child) {
			return Value!.Equals(child) || Children.Any(t => t.Contains(child));
		}

		public Tree<T>? GetChild(T child) {
			if (Value!.Equals(child)) {
				return this;
			} else {
				var childWithIt = Children.Find(c => c.Contains(child));
				if (childWithIt != null) {
					return childWithIt.GetChild(child);
				} else {
					return null;
				}
			}
		}
	}
}

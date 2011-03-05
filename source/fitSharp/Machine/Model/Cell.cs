﻿// Copyright © 2011 Syterra Software Inc. All rights reserved.
// The use and distribution terms for this software are covered by the Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
// which can be found in the file license.txt at the root of this distribution. By using this software in any fashion, you are agreeing
// to be bound by the terms of this license. You must not remove this notice, or any other, from this software.

namespace fitSharp.Machine.Model {
    public enum CellAttribute {
        Actual,
        Add,
        Body,
        EndTag,
        Exception,
        Extension,
        Formatted,
        InformationPrefix,
        InformationSuffix,
        Label,
        Leader,
        Raw,
        StartTag,
        Status,
        Trailer
    }

    public interface Cell {
        string Text { get; }
        bool HasAttribute(CellAttribute key);
        string GetAttribute(CellAttribute key);
        void SetAttribute(CellAttribute key, string value);
        void AddToAttribute(CellAttribute key, string value);
        TypedValue Value { get; set; }
    }
}

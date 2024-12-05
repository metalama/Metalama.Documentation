// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

namespace DebugDemo
{
    public class Student
    {
        public void ChangeCourse( string newCourse ) { }

        public decimal GetCourseFee( string courseID )
        {
            //TODO
            return 0;
        }
    }

    public class Department
    {
        private decimal _calculateCostHelper()
        {
            //TODO
            return 0;
        }

        public decimal CalculateCost()
        {
            //TODO
            return 1.5M * this._calculateCostHelper();
        }
    }
}
export class SmartRelease{
    constructor(
        public releaseID?: number,
        public releaseDate?: Date,
        public amountAccrued?: number,
        public amountPaid?: number,
        public employeeFullName?: string
    ) { }
  }
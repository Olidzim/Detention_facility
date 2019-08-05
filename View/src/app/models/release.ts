export class Release {
    constructor(
        public releaseID?: number,
        public detaineeID?: number,
        public detentionID?: number,
        public releaseDate?: Date,
        public releasedByEmployeeID?: number,
        public amountAccrued?: number,
        public amountPaid?: number       
    ) { }
  }
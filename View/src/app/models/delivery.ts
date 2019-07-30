export class Delivery {
    constructor(
        public deliveryID?: number,
        public detaineeID?: number,
        public detentionID?: number,
        public deliveryDate?: Date,
        public placeAddress?: string,
        public deliveredByEmployeeID?: number,
    ) { }
  }
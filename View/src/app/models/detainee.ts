export class Detainee {
    constructor(
        public detaineeID?: number,
        public firstName?: string,
        public lastName?: string,
        public patronymic?: string,
        public birthDate?: Date,
        public maritalStatus?: string,
        public job?: string,
        public mobilePhoneNumber?: string,
        public homePhoneNumber?: string,   
        public photo?: string,
        public extraInfo?: string,
        public residencePlace?: string
    ) { }
  }

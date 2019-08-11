export class ResponseClass {
    constructor(
        public  isSuccess?: boolean,
        public  message?: string,
        public  responseData?: object, 
    ) { }
  }
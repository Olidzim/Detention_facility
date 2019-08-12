import { PermissionType } from '../permission-type';
import { PermissionBase } from './base.permissions';

export class AdminPermission extends PermissionBase {

  constructor() {
    super();
    this.permissions = [  
      PermissionType.READ,
      PermissionType.UPDATE,
    ];
  }
}

import { PermissionBase } from './base.permissions';
import { Role } from '../role';
import { EditorPermission } from './editor.permissions';
import { AdminPermission } from './admin.permissions';
import { UserPermission } from './user.permissions';
import { UnknownPermission } from './unknown.permissions';

export class PermissionsFactory {

  public static instance: PermissionBase;
  private constructor() {}

  public static getInstance() {   
   /* if (this.instance) {
      return this.instance;
    } else {*/
      const role = localStorage.getItem('role');
      switch(role) {
        case Role.ADMIN:
         this.instance = new AdminPermission();
         break;
        case Role.EDITOR: 
        {        
         this.instance = new EditorPermission();
          break;  
        }            
        case Role.USER:
          this.instance = new UserPermission();
          break;
        default:
          this.instance = new UnknownPermission();
          break;
    //  }
    }
    return this.instance;
  }
}
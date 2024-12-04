import { CanDeactivateFn } from '@angular/router';
import { Member } from '../_models/members';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

export const preventUnsavedChangesGuard: CanDeactivateFn<MemberEditComponent> = (component) => {
  if(component.editForm?.dirty)
  {
    return confirm('Are you sure you want to leave this page? Any unsaved changes will be lost.');
  }
  return true;  
};

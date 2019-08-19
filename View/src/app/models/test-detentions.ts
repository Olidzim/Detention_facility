import { SmartDetention } from './smartdetention';

/** return fresh array of test heroes */
export function getTestDetentions(): SmartDetention[] {
  return [
    {detentionID: 1, detentionDate: new Date, employeeFullName: "QW"},
    {detentionID: 2, detentionDate: new Date, employeeFullName: "ER"},
    {detentionID: 3, detentionDate: new Date, employeeFullName: "TY"}
  ];
}
import { Currency } from "./currency";

export interface Place {
  id: number,
  createDate: Date,
  createdBy: string,
  lastModifiedDate: Date,
  modifiedBy: string,
  name: string,
  description: string,
  locationId: number,
  location: Location,
  link: string,
  phoneNumber: string,
  image: string,
  cost: number,
  averageDuration: string,
  currency: Currency,
  rating: number
}
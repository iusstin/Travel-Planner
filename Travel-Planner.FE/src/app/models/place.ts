import { Currency } from "./currency";

export interface Place {
    id: number,
    createDate: Date,
    createdBy: string,
    lastModifiedDate: Date,
    modifiedBy: string,
    name: string,
    description: string,
    address: string,
    city: string,
    country: string,
    latitude: string,
    longitude: string,
    link: string,
    phoneNumber: string,
    image: string,
    cost: number,
    averageDuration: string,
    currency: Currency
  }
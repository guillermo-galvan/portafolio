import { DeliveryMan } from './DeliveryMan';
import { TimeList } from './TimeList';

export type ResponseDealer = {
    success: boolean;
    deliveries: DeliveryMan[];
    dates: TimeList;
}
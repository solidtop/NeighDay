import { UserSummary } from '../../account/models/user-summary.model';

export interface ChatMessage {
  id?: string;
  text: string;
  textColor?: string;
  timestamp?: Date;
  user?: UserSummary;
}

import { ChatCommand } from './chat-command.model';

export interface ChatMessageRequest {
  text: string;
  channelId: string;
  command?: ChatCommand;
}

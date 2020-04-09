export interface APIResponse {
  [index: number]: BrokerInfo
}

export interface BrokerInfo{
  name: string;
  count: string;
}

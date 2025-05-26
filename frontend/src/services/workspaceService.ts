export const workspaceService = {
  async getUserWorkspaces(userId: number): Promise<Workspace[]> {
    const response = await fetch(`http://localhost:5248/api/WorkSpace/user/${userId}`);
    if (!response.ok) throw new Error('Failed to fetch workspaces');
    return await response.json();
  },

  async createWorkspace(name: string, description: string, userId: number): Promise<number> {
    const response = await fetch('http://localhost:5248/api/WorkSpace/create', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ 
        UserId: userId,
        Name: name,
        Description: description 
      }),
    });
    if (!response.ok) throw new Error('Failed to create workspace');
    const data = await response.json();
    return data.id;
  }
};

export interface Workspace {
  id: number;
  name: string;
  accessLevel: string;
}
// src/services/workspaceService.ts
export interface Block {
  id: number;
  content: string;
  type: string;
  order: number;
  pageId: number;
}

export const workspaceService = {
  // Получение всех блоков страницы
  async getPageBlocks(pageId: number): Promise<Block[]> {
    const response = await fetch(`http://localhost:5248/api/page/${pageId}`);
    if (!response.ok) throw new Error('Ошибка загрузки страницы');
    const data = await response.json();
    
    // Дебаг-проверка
    console.log('Ответ от /api/page:', {
        status: response.status,
        data: data
    });

    // Проверяем оба варианта (Blocks/Block)
    return data.Blocks  
},


  // Создание нового блока
  async createBlock(data: {
    content: string;
    type: string;
    order: number;
    pageId: number;
  }): Promise<Block> {
    const response = await fetch('http://localhost:5248/api/block/create', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data),
    });
    if (!response.ok) throw new Error('Ошибка создания блока');
    return await response.json();
  },

  // Обновление блока
  async updateBlock(blockId: number, content: string): Promise<void> {
    const response = await fetch('http://localhost:5248/api/block/update', {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ Id: blockId, Content: content }),
    });
    if (!response.ok) throw new Error('Ошибка обновления блока');
  },

  // Удаление блока
  async deleteBlock(blockId: number): Promise<void> {
    const response = await fetch(`http://localhost:5248/api/block/${blockId}`, {
      method: 'DELETE',
    });
    if (!response.ok) throw new Error('Ошибка удаления блока');
  },
};
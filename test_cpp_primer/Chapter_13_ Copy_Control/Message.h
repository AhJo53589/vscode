#pragma once

#include <set>

class Folder;
class Message
{
	friend class Folder;
	friend void swap(Message &lhs, Message &rhs);
public:
	explicit Message(const std::string &str = "")
		: contents(str) {}
	Message(const Message&);
	Message& operator= (const Message&);
	Message(Message &&);
	Message& operator= (Message&&);
	~Message();
	
	void save(Folder&);
	void remove(Folder&);

	void show();

private:
	std::string contents;
	std::set<Folder*> folders;

	void add_to_Folders(const Message&);
	void remove_from_Folders();
	void move_Folders(Message *);
};


class Folder
{
	friend class Message;
public:
	static int FolderID;

	explicit Folder()
		: folder_id(FolderID++) {}
	Folder(const Folder&);
	Folder& operator= (const Folder&);
	~Folder();

	void addMsg(Message*);
	void remMsg(Message*);

	void show();

private:
	int folder_id;
	std::set<Message*> messages;

	void add_all_messages(const Folder&);
	void remove_all_messages();
};

